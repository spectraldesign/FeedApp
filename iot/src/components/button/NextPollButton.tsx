import { Component, createSignal } from "solid-js";
import './Button.css'
import Votescreen, { setOnePoll, setPoll, poll, setPoll_id, onePoll, poll_id  } from '../../routes/Votescreen';
import axios from 'axios';
import { useNavigate } from '@solidjs/router';
const [count, setCount] = createSignal(0);

const NextPollButton: Component = () => {
    const navigate = useNavigate();
    async function onClick(){
        if(count() >= poll().length-1){
            
            setCount(0);
            alert("Could not find more polls, redirecting to home screen");
            navigate('/');
            return console.log('No more polls :)')
        }
        setCount(count() + 1);
        setOnePoll(poll()[count()]);
        setPoll_id(onePoll()["id"]);
        }

    return (
        <button class="next-poll-btn btn" onClick={onClick}> Next Poll </button>
    )
};   
export default NextPollButton;