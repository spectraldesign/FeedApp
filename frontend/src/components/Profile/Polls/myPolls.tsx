import { createSignal, For } from "solid-js";
import { useNavigate, NavLink } from '@solidjs/router';
import DefaultPoll from "./defaultPoll/defaultPoll";
import "./myPolls.css";

const [pollId, setPollId] = createSignal('');

function MyPolls() {
    const [myPolls, setMyPolls] = createSignal([{id: ''}]);

    const [poll, setPoll] = createSignal({});
    const token = localStorage.getItem("token");
    var authentic = token?.substring(1, token.length-1);

    const navigate = useNavigate();
    fetch(`${import.meta.env.VITE_BASE_URL}poll/myPolls`, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*',
                    'Authorization': 'Bearer ' + authentic,
                },
                
                // body: JSON.stringify(data)
            })
            .then(response => {
                if (response.status === 200) {
                    console.log(response);
                    // alert('Get request success');
                    return response.json();
                } else {
                    alert('Get request success invalid');
                }
            }
        )
            .then(data => {
                setMyPolls(data);
                console.log(myPolls());
            })

    return (
        <div class="trial">
            <div class="my-polls">
                <h1 class="poll-header">My polls</h1>
            </div>
            <For each={myPolls()}>{(myPolls, i) =>
              <div>
                <DefaultPoll id={myPolls.id}/>
              </div>
            }</For>
        </div>
    );
}

export { pollId, setPollId };
export default MyPolls;