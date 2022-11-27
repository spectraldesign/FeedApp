import { Component } from "solid-js";
import "../../button.css"

const MyPollButton: Component = () => {
    return (
        <div class="button createpoll">
            <a href="/poll/create">Create Poll</a>
        </div>
    );
  }

  export default MyPollButton;

