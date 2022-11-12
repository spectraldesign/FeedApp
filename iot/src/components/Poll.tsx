import Buttons from './button/Buttons';
import './Poll.css';
import { onePoll, iot_device } from '../routes/Votescreen';
import NextPollButton from './button/NextPollButton';
import { Component } from 'solid-js';

const Poll:Component = () => {

    if (onePoll() !== undefined) {
        return (
            <div class="container">
            <div class="poll">
                <div class="poll-form">
                    <h1 class='text-center'> IoT Device ID: {iot_device()["deviceID"]} </h1>
                    <h1 class='text-center'> IoT Device Name: {iot_device()["deviceName"]} </h1>
                    <p class='poll-text'> Question: </p>
                    <p class='poll-text'> {onePoll()["question"]} </p>
                    <Buttons />
                </div>
            </div>
        </div>
        );
    }
    else {
    return (
        <div class="container">
            <div class="poll">
                <div class="poll-form">
                    <h1 class='text-center'> IoT Device: {iot_device()["deviceName"]} </h1>
                    <p class='poll-text'> Could not find more polls </p>
                </div>
            </div>
        </div>
        );
    }
}

export default Poll;

