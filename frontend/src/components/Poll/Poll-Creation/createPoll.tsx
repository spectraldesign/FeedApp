import { formatPostcssSourceMap } from "vite";
import "./createPoll.css";
import { createForm } from './CreatePollForm';
import { DateTimePicker } from 'date-time-picker-solid'
import toast from "solid-toast";
import { useNavigate, NavLink } from '@solidjs/router';

function CreatePoll() {
    const { form, updateFormField, submit, setForm } = createForm();
    const token = localStorage.getItem("token");
    let authentic = token?.substring(1, token.length-1);
    const navigate = useNavigate();
    const handleSubmit = (e: Event) => {
        const data = submit(form);
        console.log(data);
        e.preventDefault();
        fetch(`${import.meta.env.VITE_BASE_URL}poll`, {
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
                    toast.success("Poll created", {position:"bottom-center", style: {'background-color': '#cdf2cb',}})
                    navigate("/")
                    return response.text();
                } else {
                    toast.error("Invalid input", {position:"bottom-center", style: {'background-color': '#f2cbcb',}})
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
                    <div class="dateTimeDiv">
                        <DateTimePicker 
                        minDate={new Date(Date.now())} 
                        currentDate={new Date(Date.now())} 
                        enableTimeView
                        dateFormat="DD MMM, YYYY @ HH:mm"
                        closeOnSelect={true}
                        calendarResponse={x=>{
                            console.log(x); 
                            setForm({["endTime"]: x.currentDate});
                        }}
                        />
                    </div>
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

