import { Component } from "solid-js";
import NextPollButton from "./NextPollButton";
import VoteNoButton from "./VoteNoButton";
import VoteYesButton from "./VoteYesButton";
import './Button.css'

const Buttons : Component = () => {

    
    return (
        <div class='buttons'> 
            <VoteYesButton />
            <VoteNoButton />
            <NextPollButton />
        </div>
    )
};

export default Buttons;