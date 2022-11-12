import { Component, createSignal } from "solid-js";
import './Button.css'
import { setOnePoll, poll, setPoll_id, onePoll, poll_id  } from '../../routes/Votescreen';
import axios from 'axios';
const [count, setCount] = createSignal(0);

const NextPollButton: Component = () => {

    
    const nextPoll = async () => {
        setCount(count() + 1);
        setOnePoll(poll()[count()]);
        setPoll_id(onePoll()["id"]);
        console.log("poll_id", poll_id());
        console.log("onePoll", onePoll());
        }

    return (
        <button class="next-poll-btn btn" onClick={nextPoll}> Next Poll </button>
    )
};   
export default NextPollButton;