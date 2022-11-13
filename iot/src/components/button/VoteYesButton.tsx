import { Component } from "solid-js";
import './Button.css'
import axios from 'axios';
import { poll_id } from '../../routes/Votescreen';

const VoteYesButton: Component = () => {
    const voteYes = async () => {
        console.log("Poll sin id", poll_id());
        const res = await axios.post(`${import.meta.env.VITE_BASE_URL}vote/${poll_id()}`, {
            "isPositive": true
        })
        const data = await res.data.status;
        console.log(data);
        console.log("Ja");  

    }
    return (
        <button class="vote-yes-btn btn" onClick={voteYes}> Yes </button>
    )
};   
export default VoteYesButton;