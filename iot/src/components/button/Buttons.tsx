import { Component } from "solid-js";
import NextPollButton from "./NextPollButton";
import VoteNoButton from "./VoteNoButton";
import VoteYesButton from "./VoteYesButton";
import './Button.css'

const Buttons : Component = () => {

    
    return (
        <div class='buttons'> 
            <div>
                <VoteNoButton />
                <VoteYesButton />
            </div>
            <div>
                <NextPollButton />
            </div>
        </div>
    )
};

export default Buttons;