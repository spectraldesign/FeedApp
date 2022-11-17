import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "../../button.css"

function CreatePollButton() {
    return (
        <div class="button container">
            <div class="button createpoll">
                <a href="/poll/create">Create Poll</a>
            </div>
            <div class="button mypolls">
                <a href="/profile/polls">My polls</a>
            </div>
        </div>
    );
  }

  export default CreatePollButton;

