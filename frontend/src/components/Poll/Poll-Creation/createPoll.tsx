import "./createPoll.css";

function CreatePoll() {
    return (
        <div class="trial">
            <div class="poll-create">
                <form class="poll-create-form" action="">
                    <h2 class="poll-creation">Poll creation</h2>
                    <input class="poll-create-input" type="text" id="question" name="question" placeholder="&#128511; Question"></input>
                    <input class="poll-create-input" type="text" id="time" name="time" placeholder="&#128337; Time Limit"></input>
                    <input class="create-poll-btn" type="submit" value="Create Poll"></input>
                </form>
            </div>
        </div>
    );
  }

  export default CreatePoll;

