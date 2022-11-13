import { useNavigate } from "@solidjs/router";
import { createSignal, Show } from "solid-js";
import "./defaultPoll.css";

const polls_JSON = '{"Id":"0c761621-7c3e-4b4f-90ae-028cadf0a817", "Question": "Is this a poll?", "IsPrivate":true, "IsClosed":false, "EndTime":"2022-11-04 18:37:26.997+00", "CreatorId":"cd12d700-030a-4ef7-a836-e8e99d28a00d"}';
const polls_Obj = JSON.parse(polls_JSON);

const votes_JSON = '{"Id":"0c761621-7c3e-4b4f-90ae-028cadf0a817", "total_votes":"500", "yes_votes":"190", "no_votes":"310"}';
const votes_Obj = JSON.parse(votes_JSON);

export default function DefaultPoll(props: any) {
    const navigate = useNavigate();

    const [poll, setPoll] = createSignal('');
    const token = localStorage.getItem("token");
    var authentic = token?.substring(1, token.length-1);

    fetch(`https://localhost:7280/api/poll/${props.id}`, {
            method: 'GET',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Authorization': 'Bearer ' + authentic,
            },
        })
        .then(response => {
            if (response.status === 200) {
                console.log(response);
                return response.json();
            } else {
                alert('Invalid fetch');
                console.log(response);
            }
        })
        .then(data => {
            setPoll(data);
            console.log(poll());
            console.log(poll()['isClosed']);
        })

    const handleSubmit = () => {
        fetch(`https://localhost:7280/api/poll/${props.id}/close`, {
                method: 'PUT',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': 'Bearer ' + authentic,
                },
        })
        .then(response => {
            if (response.status === 200) {
                console.log(response);
                alert('Delete poll success');
                navigate('/profile/polls');
                return response.json();
            } else {
                alert('Delete poll fail');
                navigate('/profile/polls');
            }
        })
    };

    return (
        
        <div class="trial">
            <div class="default-poll">
                <form class="poll-result-form" action="" onSubmit={handleSubmit}>
                    <div class ="all-elements">
                    <h3 class="poll-question">{poll()['question']}</h3>
                    <div class="container-result">
                        <div class="result yes" data-width={(poll()['positiveVotes'])/(poll()['positiveVotes']+poll()['negativeVotes'])*100}> {(poll()['positiveVotes'])/(poll()['positiveVotes']+poll()['negativeVotes'])*100}%</div>
                    </div>
                    <div class="container-result" >
                        <div class="result no" data-width={(poll()['negativeVotes'])/(poll()['positiveVotes']+poll()['negativeVotes'])*100}>{(poll()['negativeVotes'])/(poll()['positiveVotes']+poll()['negativeVotes'])*100}%</div> 
                    </div>
                    <div class="text-and-button">
                        <p id="text"> {(poll()['positiveVotes']+poll()['negativeVotes'])} overall votes  |  Poll closes at {poll()['endTime']} </p>
                    </div>
                    <Show
                        when={poll()['isClosed']}
                        fallback={<input class="submit-poll-btn" type="submit" value="Close Poll"></input>}></Show>
                </div>
                </form>
            </div>
        </div>
    );
}
