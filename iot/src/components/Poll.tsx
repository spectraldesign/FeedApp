import Buttons from './button/Buttons';
import './Poll.css';
import { onePoll, iot_device } from '../routes/Votescreen';
import NextPollButton from './button/NextPollButton';
import { Component } from 'solid-js';

function Poll() {

    if (onePoll() !== undefined) {
        return (
            <div class="container">
            <div class="poll">
                <div class="poll-form">
                    <h1 class='device-id'><b>IoT Device ID:</b> {iot_device()["deviceID"]} </h1>
                    <h1 class='device-name'><b>IoT Device Name:</b> {iot_device()["deviceName"]} </h1>
                    <p class='poll-text'> Question: </p>
                    <p class='poll-question'> {onePoll()["question"]} </p>
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