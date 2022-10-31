import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./apiButton.css";

function APIButton() {
    createEffect(() => {
        console.log("Button has been pressed");
      });
    return (
        <div>
            <div class="apibutton">
                <a href="/">API</a>
            </div>
        </div>
    );
  }

  export default APIButton;

