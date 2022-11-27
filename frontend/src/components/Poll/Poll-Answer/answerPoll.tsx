import "./answerPoll.css";
import "../../button.css"
import { useNavigate } from "@solidjs/router";
import { createSignal } from "solid-js";
import { answerForm } from "./answerPollForm";
import { poll, setPoll, pollId, setPollId} from "../Poll-Search/searchPoll"; 
import { resultPollId, setResultPollId } from "../Poll-Results/resultPoll";
import toast from "solid-toast";

// const [resultPollId, setResultPollId] = createSignal('');
const [resultPoll, setResultPoll] = createSignal('');

function getPoll() {
    return localStorage.getItem("pollId");
}

function AnswerPoll() {
    const { form, updateFormField, submit, clearField } = answerForm();
    const navigate = useNavigate();

    const token = localStorage.getItem("token");
    var authentic = token?.substring(1, token.length-1);

    const handleSubmit = (e: Event) => {
        e.preventDefault();
        if (form.isPositive === undefined) {
            toast.error("Please select an option", {position:"bottom-center", style: {'background-color': '#f2cbcb',}})
        }
        else {
        const data = submit(form);
        console.log("data, ", data);
    
        fetch(`${import.meta.env.VITE_BASE_URL}vote/${pollId()}`, {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': 'Bearer ' + authentic
                },
                body: JSON.stringify(data)
            })
            .then(response => {
                console.log("response status: " + response.status);
                console.log()
                if (response.status === 201) {
                    setResultPollId(pollId());
                    setResultPoll(poll());
                    navigate(`/poll/${pollId()}/results`)
                    return response.text();
                } 
                else if (response.status === 403){
                    toast.error("Cannot vote on private poll twice", {position:"bottom-center", style: {'background-color': '#f2cbcb',}})
                }
                else {
                    toast.error("Invalid vote", {position:"bottom-center", style: {'background-color': '#f2cbcb',}})
                }
            }
        )
        }
        
    }

    return (
        
        <div class="trial">
            <div class="poll-answer">
                <form class="poll-answer-form" action="" onSubmit={handleSubmit}>
                    <div class ="all-elements">
                    <h1 class="poll-question">{poll()['question']}</h1>                    
                    <div class="radio-buttons">
                        <input 
                            type="radio" 
                            name="isPositive" 
                            id="positive"
                            value="true"
                            onChange={updateFormField('isPositive')}/>
                        <label for="positive">Yes</label>
                        <input 
                            type="radio" 
                            name="isPositive" 
                            id="negative"
                            value="false"
                            onChange={updateFormField('isPositive')}/>
                        <label for="negative">No</label>
                        <p id="text"> {poll()['positiveVotes'] + poll()['negativeVotes']} overall votes  |  Poll closes at {poll()['endTime']} </p>
                        <input class="submit-btn"  type="submit" value="Vote"></input>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
}

export { resultPoll, setResultPoll};
export default AnswerPoll;