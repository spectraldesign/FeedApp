import { Component, createEffect, createResource, createSignal } from 'solid-js';
import { Routes, Route } from '@solidjs/router';
//import { id } from '../App';
import axios from 'axios';

const [id, setId] = createSignal('');
const [iot_device, setIot_device] = createSignal('');

function Votescreen (){

    return (
        <div>
            <p> Hello you </p>
            <h1> Votescreen </h1>
            <p> {iot_device()["deviceName"]} </p>
            <p> {iot_device()["deviceID"]}</p>
        </div>
    )
}

export { id, setId, iot_device, setIot_device };
export default Votescreen;