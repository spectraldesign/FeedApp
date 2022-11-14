import { formatPostcssSourceMap } from "vite";
import "./createPoll.css";
import { createForm } from './CreatePollForm';

function CreatePoll() {
    const { form, updateFormField, submit, clearField } = createForm();
    const token = localStorage.getItem("token");
    var authentic = token?.substring(1, token.length-1);

    const handleSubmit = (e: Event) => {
        const data = submit(form);
        console.log(data);
        e.preventDefault();
        fetch('https://localhost:7280/api/poll', {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': 'Bearer ' + authentic,
                },
                body: JSON.stringify(data)
            })
            .then(response => {
                if (response.status === 201) {
                    console.log(response);
                    alert('Poll created');
                    return response.text();
                } else {
                    alert('Invalid input');
                    console.log(response);
                }
            }
        )
    }

    

    return (
        <div class="trial">
            <div class="poll-create">
                <form class="poll-create-form" onSubmit={handleSubmit}>
                    <h2 class="poll-creation">Poll creation</h2>
                    <input 
                        type="text" 
                        class="poll-create-input" 
                        id="question" 
                        placeholder="&#128511; Question"
                        value={form.question}
                        onInput={updateFormField('question')}
                        required
                        />
                    <input 
                        type="text" 
                        class="poll-create-input" 
                        id="time" 
                        placeholder="&#128337; Time Limit"
                        value={form.endTime}
                        onInput={updateFormField('endTime')}
                        required
                        />
                    <div class="privacy">
                        <label class="privacy"><h3>Private Poll: </h3>
                        <input 
                            type="checkbox" 
                            name="privacy-check" 
                            id="privacy-check"
                            onChange={updateFormField('isPrivate')}/>
                        </label>
                    </div>
                    <input class="create-poll-btn" type="submit" value="Create Poll"></input>
                </form>
            </div>
        </div>
    );
  }

  export default CreatePoll;

