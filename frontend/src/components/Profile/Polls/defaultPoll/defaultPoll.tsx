import { useNavigate } from "@solidjs/router";
import { createSignal, Show } from "solid-js";
import "./defaultPoll.css";

export default function DefaultPoll(props: any) {
    const navigate = useNavigate();

    const [poll, setPoll] = createSignal('');
    const [countVotes, setCountVotes] = createSignal(0);
    const [positiveVotes, setPositiveVotes] = createSignal(0);
    const [negativeVotes, setNegativeVotes] = createSignal(0);
    const [positivePercent, setPositivePercent] = createSignal(0);
    const [negativePercent, setNegativePercent] = createSignal(0);

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
            setPositiveVotes(data['positiveVotes']);
            setNegativeVotes(data['negativeVotes']);
            setCountVotes(positiveVotes() + negativeVotes());
            if (countVotes() == 0) {
                setPositivePercent(0);
                setNegativePercent(0);
            }
            else {
                setPositivePercent(positiveVotes()/countVotes()*100);
                setNegativePercent(negativeVotes()/countVotes()*100);
            }
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
                        <div class="result-yes" style={{
                            width: `${positivePercent().toFixed()}%`
                        }}> {positivePercent().toFixed()}%</div>
                    </div>
                    <div class="container-result" >
                        <div class="result-no" style={{
                            width: `${negativePercent().toFixed()}%`
                        }}>{negativePercent().toFixed()}%</div> 
                    </div>
                    <div class="text-and-button">
                        <p id="text"> {(poll()['positiveVotes']+poll()['negativeVotes'])} overall votes  |  <Show
                        when={!poll()['isClosed']}
                        fallback={<span>Poll is closed</span>}><span> Poll closes at {poll()['endTime']}</span></Show> </p>
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
