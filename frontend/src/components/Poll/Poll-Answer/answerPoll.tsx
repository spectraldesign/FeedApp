import "./answerPoll.css";
import { useNavigate } from "@solidjs/router";
import { createSignal } from "solid-js";
import { answerForm } from "./answerPollForm";
import { poll, setPoll, pollId, setPollId} from "../Poll-Search/searchPoll"; 
import { resultPollId, setResultPollId } from "../Poll-Results/resultPoll";

// const [resultPollId, setResultPollId] = createSignal('');
const [resultPoll, setResultPoll] = createSignal('');


function AnswerPoll() {
    const { form, updateFormField, submit, clearField } = answerForm();
    const navigate = useNavigate();

    const token = localStorage.getItem("token");
    var authentic = token?.substring(1, token.length-1);

    // fetch(`https://localhost:7280/api/poll/${pollId()}`, {
    //         method: 'GET',
    //         mode: 'cors',
    //         headers: {
    //             'Content-Type': 'application/json',
    //             'Access-Control-Allow-Origin': '*'
    //         },
    //     })
    //     .then(response => {
    //         if (response.status === 200) {
    //             console.log(response);
    //             return response.json();
    //         } else {
    //             alert('Invalid fetch');
    //         }
    //     })
    //     .then(data => {
    //         setPoll(data);
    //         setResultPollId(pollId());
    //     })

    const handleSubmit = (e: Event) => {
        const data = submit(form);
        e.preventDefault();
        fetch(`https://localhost:7280/api/vote/${pollId()}`, {
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
                if (response.status === 201) {
                    console.log(response);
                    setResultPollId(pollId());
                    setResultPoll(poll());
                    navigate('/poll/results')
                    return response.text();
                } 
                else if (response.status === 403){
                    console.log(response);
                    alert("Cannot vote on same poll twice");
                }
                else {
                    console.log(response);
                    alert('Invalid vote');
                }
            }
        )
        
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
                        <input class="submit-answer-btn"  type="submit" value="Vote"></input>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    );
}

export { resultPoll, setResultPoll};
export default AnswerPoll;