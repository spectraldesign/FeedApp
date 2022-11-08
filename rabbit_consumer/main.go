package main

import (
	"bytes"
	"context"
	"encoding/json"
	"fmt"
	"log"
	"net/http"
	"time"

	amqp "github.com/rabbitmq/amqp091-go"
	"go.mongodb.org/mongo-driver/mongo"
	"go.mongodb.org/mongo-driver/mongo/options"
)

func failOnError(err error, msg string) {
	if err != nil {
		log.Panicf("%s: %s", msg, err)
	}
}

type Vote struct {
	Id       int
	Positive bool
}

type Poll struct {
	Id        string
	Question  string
	IsPrivate bool
	IsClosed  bool
	EndTime   time.Time
	Votes     []Vote
}

type PollResult struct {
	Id            string
	Question      string
	PositiveVotes int
	NegativeVotes int
	TotalVotes    int
}

func main() {
	// Connect to MongoDB
	serverAPIOptions := options.ServerAPI(options.ServerAPIVersion1)
	clientOptions := options.Client().
		ApplyURI("mongodb+srv://myadmin:Isergodt123@cluster0.psuxl12.mongodb.net/?retryWrites=true&w=majority").
		SetServerAPIOptions(serverAPIOptions)
	ctx, cancel := context.WithTimeout(context.Background(), 10*time.Second)
	defer cancel()
	client, err := mongo.Connect(ctx, clientOptions)
	if err != nil {
		log.Fatal(err)
	}
	defer client.Disconnect(ctx)

	conn, err := amqp.Dial("amqp://guest:guest@localhost:5672/")
	failOnError(err, "Failed to connect to RabbitMQ")
	defer conn.Close()

	ch, err := conn.Channel()
	failOnError(err, "Failed to open a channel")
	defer ch.Close()

	q, err := ch.QueueDeclare(
		"polls", // name
		false,   // durable
		false,   // delete when unused
		false,   // exclusive
		false,   // no-wait
		nil,     // arguments
	)
	failOnError(err, "Failed to declare a queue")

	msgs, err := ch.Consume(
		q.Name, // queue
		"",     // consumer
		true,   // auto-ack
		false,  // exclusive
		false,  // no-local
		false,  // no-wait
		nil,    // args
	)
	failOnError(err, "Failed to register a consumer")

	var forever chan struct{}

	go func() {
		for d := range msgs {

			if d.MessageId == "poll_created" {
				var poll Poll
				json.Unmarshal([]byte(d.Body), &poll)
				log.Printf("Poll %s is created", poll.Id)
				log.Printf("Push result to Dweet.io")

				responseBody := bytes.NewBuffer(d.Body)
				resp, err := http.Post("https://dweet.io/dweet/for/feed-app-poll-result", "application/json", responseBody)

				if err != nil {
					log.Printf("Error posting to Dweet.io: %s", err)
				}
				defer resp.Body.Close()
			} else {
				var pollResult PollResult
				json.Unmarshal([]byte(d.Body), &pollResult)
				log.Printf("Poll %s is closed", pollResult.Id)

				coll := client.Database("polls").Collection("polls_result")
				result, err := coll.InsertOne(context.TODO(), pollResult)
				fmt.Printf("Inserted document with _id: %v\n", result.InsertedID)

				if err != nil {
					log.Fatal(err)
				}
			}
		}
	}()

	log.Printf(" [*] Waiting for messages. To exit press CTRL+C")
	<-forever
}
