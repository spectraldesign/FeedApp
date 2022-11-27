import "./SearchPoll.css";
import { loginForm } from '../../Login/LoginForm';
import { useNavigate, NavLink, useParams } from '@solidjs/router';
import { createSignal } from "solid-js";
import toast from "solid-toast";

const [poll, setPoll] = createSignal('');
const [pollId, setPollId] = createSignal('');

function SearchPoll() {
    const { titleParam  } = useParams();

    const refreshPage = () => {
        window.location.reload();
    }

    const { form, updateFormField, submit, clearField } = loginForm();
    const navigate = useNavigate();

    const handleChange = (e: any) => {
        setPollId(e.target.value);
        localStorage.setItem("pollId", e.target.value);
    }

    const handleSubmit = (e: Event) => {
        const input = document.getElementById('fname') as HTMLInputElement | null;
        let value = input?.value;
        if(!value){value = "_"}
        console.log(value)

        // const data = submit(form);
        e.preventDefault();
        fetch(`${import.meta.env.VITE_BASE_URL}poll/${value}`, {
                method: 'GET',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*'
                },
        })
        .then(response => {
            if (response.status === 200) {
                navigate('/');
                return response.json();
            } else {
                toast.error("Poll not found", {position:"bottom-center", style: {'background-color': '#f2cbcb',}})
            }
        })
        .then(data => {
            if (data["isClosed"]) {
                toast.error("Poll is closed", {position:"bottom-center", style: {'background-color': '#f2cbcb',}})
            }
            else {
                if (data["isPrivate"] && localStorage.getItem("loggedIn")) {
                    setPoll(data);
                    navigate(`/polls/${pollId()}`);
                }
                else if (data["isPrivate"] && !(localStorage.getItem("loggedIn"))) {
                    toast.error("Private poll, please log in to vote", {position:"bottom-center", style: {'background-color': '#f2cbcb',}})
                    navigate('/login');
                }
                else {
                    console.log(data)
                    setPoll(data);
                    navigate(`/polls/${pollId()}`);
                }
            }
        })
    };

    return (
        <div class="trial">
            <div class="poll-search">
                <form class="poll-search-form" action="" onSubmit={handleSubmit}>
                    <img class="logo" src="https://media.discordapp.net/attachments/579830395412152334/1045834936323674162/feedapp.png?width=1080&height=1080"></img>
                    <input class="poll-search-input" type="text" id="fname" name="firstname" onChange={handleChange} placeholder="&#128269; Enter Poll ID"></input>
                    <input class="submit-btn" type="submit" value="Enter"></input>
                </form>
            </div>
        </div>
    );
  }

  export { poll, setPoll, pollId, setPollId};
  export default SearchPoll;

