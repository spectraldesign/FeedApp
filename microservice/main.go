package main

import (
	"bytes"
	"context"
	"encoding/json"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"time"

	"github.com/google/uuid"

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
	Id        uuid.UUID
	Question  string
	IsPrivate bool
	IsClosed  bool
	EndTime   time.Time
	Votes     []Vote
}

type PollResult struct {
	Id            uuid.UUID
	Question      string
	PositiveVotes int
	NegativeVotes int
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
		"Polls", // name
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
			log.Print(d.Body)
			var jsonString = []byte(d.Body)
			log.Printf(string(jsonString))
			var poll Poll
			json.Unmarshal([]byte(d.Body), &poll)
			//b, _ := json.Marshal(poll)

			fmt.Print(poll)
			if poll.IsClosed {
				log.Printf("Poll %s is closed", poll.Id)
				log.Printf("saving result to database")
				log.Printf("push result to Dweet.io")
				//var url = fmt.Sprintf("https://dweet.io/dweet/for/feed-app-poll-result?pollId=%s&question=%s&positivevotes=%d&negativevotes=%d",
				//	poll.Id, poll.Question, 0, 0)
				//log.Printf("Posting poll result to %s", url)
				//log.Printf("Poll result: %s", poll)
				//map
				responseBody := bytes.NewBuffer(d.Body)
				resp, err := http.Post("https://dweet.io/dweet/for/feed-app-poll-result", "application/json", responseBody)

				if err != nil {
					log.Printf("Error posting to Dweet.io: %s", err)
				}
				defer resp.Body.Close()
				fmt.Println("response Status:", resp.Status)
				fmt.Println("response Headers:", resp.Header)
				body, _ := ioutil.ReadAll(resp.Body)
				fmt.Println("response Body:", string(body))
				//Read the response body
			} else {
				log.Printf("Poll %s is open", poll.Id)
				log.Printf("saving result to MongoDB")
			}
			log.Printf("Pushing poll to MongoDB")
			coll := client.Database("polls").Collection("polls_created")
			result, err := coll.InsertOne(context.TODO(), poll)
			fmt.Printf("Inserted document with _id: %v\n", result.InsertedID)
			if err != nil {
				log.Fatal(err)
			}
		}
	}()

	log.Printf(" [*] Waiting for messages. To exit press CTRL+C")
	<-forever
}
