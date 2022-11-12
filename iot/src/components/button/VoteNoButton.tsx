import { Component } from "solid-js";
import './Button.css'
import { poll_id } from '../../routes/Votescreen';
import axios from 'axios';

const VoteNoButton: Component = () => {
    const voteNo = async () => {
        console.log("Poll sin id", poll_id());
        const res = await axios.post(`https://localhost:7280/api/vote/${poll_id()}`, {
            "isPositive": false
        }) 
    }
    return (
            <button class="vote-no-btn btn" onClick={voteNo}> No </button>
    )
};   
export default VoteNoButton;