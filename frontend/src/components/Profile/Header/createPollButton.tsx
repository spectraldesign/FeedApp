import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./createPollButton.css";

function CreatePollButton() {
    
    return (
        <div class="container">
            <div class="create-btn">
                <a href="/poll/create">Create Poll</a>
            </div>
            <div class="my-poll-btn">
                <a href="/profile/polls">My polls</a>
            </div>
        </div>
    );
  }

  export default CreatePollButton;

