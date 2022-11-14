import "./resultPoll.css";
import { createSignal } from "solid-js";

const [resultPollId, setResultPollId] = createSignal('');

function ResultPoll() {
    const [question, setQuestion] = createSignal('');
    const [endTime, setEndTime] = createSignal('');
    const [countVotes, setCountVotes] = createSignal(0);
    const [positiveVotes, setPositiveVotes] = createSignal(0);
    const [negativeVotes, setNegativeVotes] = createSignal(0);
    const [positivePercent, setPositivePercent] = createSignal(0);
    const [negativePercent, setNegativePercent] = createSignal(0);

    const token = localStorage.getItem("token");
    var authentic = token?.substring(1, token.length-1);

    console.log("test: " + resultPollId());
    fetch(`https://localhost:7280/api/poll/${resultPollId()}`, {
    // fetch(`https://localhost:7280/api/poll/1039516104483041280`, {
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
        setQuestion(data['question']);
        setEndTime(data['endTime']);
        setPositiveVotes(data['positiveVotes']);
        setNegativeVotes(data['negativeVotes']);
        setCountVotes(positiveVotes() + negativeVotes());
        setPositivePercent(positiveVotes()/countVotes()*100);
        setNegativePercent(negativeVotes()/countVotes()*100);
        // setPoll(data);
        // setIsClosed(data['isClosed']);

    })

    return (
        
        <div class="trial">
            <div class="poll-result">
                <form class="poll-result-form" action="">
                    <div class ="all-elements">
                    <h1 class="poll-question">{question}</h1>
                    <div class="container-result">
                        <div class="result-yes" style={{
                            width: `${positivePercent().toFixed()}%`
                        }}> Yes: {positivePercent().toFixed()}%</div>
                    </div>
                    <div class="container-result" >
                        <div class="result-no" style={{
                            width: `${negativePercent().toFixed()}%`
                        }}>No: {negativePercent().toFixed()}%</div> 
                    </div>
                                      
                    <div class="text-and-button2">
                        <p id="text"> {countVotes()} overall votes  |  Poll closes at {endTime()} </p>
                    </div>                    
                </div>
                
                </form>
            </div>
        </div>
    );
}
export {resultPollId, setResultPollId};
export default ResultPoll;