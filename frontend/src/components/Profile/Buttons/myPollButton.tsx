import { Component } from "solid-js";
import "../../button.css"

const MyPollButton: Component = () => {
    return (
        <div class="button mypolls">
            <a href="/profile/polls">My polls</a>
        </div>
    );
  }

  export default MyPollButton;

