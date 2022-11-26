import { createSignal, For } from "solid-js";
import { useNavigate, NavLink } from '@solidjs/router';
import "./pollList.css";
import {setPoll, setPollId} from '../Poll-Search/searchPoll'
import toast from "solid-toast";
const [pollList, setPollList] = createSignal('');

function ListPolls() {
    const navigate = useNavigate();
    const [pollList, setPollList] = createSignal([{id: '', question: '', isPrivate: '', isClosed: '', endTime: '', creatorId: '', creatorName: '', countVotes: '', positiveVotes: '', negativeVotes: ''}]);
    const token = localStorage.getItem("token");
    let authentic = token?.substring(1, token.length-1);
    fetch(`${import.meta.env.VITE_BASE_URL}poll`, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': 'Bearer ' + authentic,
                },
            })
            .then(response => {
                if (response.status === 200) {
                    return response.json();
                } else {
                    toast.error("Get request success invalid", {position:"bottom-center", style: {'background-color': '#f2cbcb',}})
                }
            }
        )
            .then(data => {
                const availablePolls = data.filter((x:any) => !x.isPrivate && !x.isClosed)
                setPollList(availablePolls);
            })

    return (
        <div class="availablePolls">
            <div class="my-polls">
                <h1 class="poll-header">All open public polls</h1>
            </div>
            <div class="divider"></div>
            <div class="polls">
                <For each={pollList()}>{(poll, i) =>
                    <div class="poll" onclick={x=>{setPoll(poll); setPollId(poll.id); navigate('/polls/:id');}}>{poll.question}</div>
                }</For>
            </div>
        </div>
    );
}

export { pollList, setPollList };
export default ListPolls;