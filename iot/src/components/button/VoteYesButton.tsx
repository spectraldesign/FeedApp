import { Component, createSignal } from "solid-js";
import './Button.css'
import axios, { AxiosResponse } from 'axios';
import Votescreen, { setOnePoll, setPoll, poll, setPoll_id, onePoll, poll_id, iot_device} from '../../routes/Votescreen';
import { useNavigate } from '@solidjs/router';
import {count, setCount} from './NextPollButton'



const VoteYesButton: Component = () => {
    const navigate = useNavigate();
    async function nextPoll(){
        //Move to next poll
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

    async function voteYes(){
        if(!poll_id()){return nextPoll()} //In this case it returns to '/'.
        //Vote
        const res:AxiosResponse = await axios.post(`${import.meta.env.VITE_BASE_URL}vote/${poll_id()}`, {
            "isPositive": true
        }).catch(err => err)
        if(!res || !res.status){return console.log('Something went wrong')}
        if(res.status != 201){return console.log(res.statusText)}

        //Remove poll from served polls
        const removePoll = await axios.post(`${import.meta.env.VITE_BASE_URL}iot/servedPolls/${iot_device()["deviceID"]}/${poll_id()}`).catch(err => err)
        if(!removePoll || !removePoll.status){return}
        if(removePoll.status != 200){return console.log(res.statusText)}
        return nextPoll();
        }
    return (
        <button class="vote-yes-btn btn" onClick={voteYes}> Yes </button>
    )
};   
export default VoteYesButton;