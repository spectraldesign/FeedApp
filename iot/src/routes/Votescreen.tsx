import { createSignal } from 'solid-js';
//import { id } from '../App';
import axios from 'axios';
import Poll from '../components/Poll';
import VoteNoButton from '../components/button/VoteNoButton';
import NextPollButton from '../components/button/NextPollButton';

const [id, setId] = createSignal('');
const [iot_device, setIot_device] = createSignal('');
const [poll, setPoll] = createSignal('');
const [onePoll, setOnePoll] = createSignal('');
const [poll_id, setPoll_id] = createSignal('');

function Votescreen (){
    return (
        <div>
            <Poll />
        </div>
    )
}

export { id, setId, iot_device, setIot_device, onePoll, poll_id, poll, setOnePoll, setPoll_id, setPoll};
export default Votescreen;