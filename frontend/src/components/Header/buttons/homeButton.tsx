import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./homeButton.css";

function HomeButton() {
    createEffect(() => {
        console.log("Button has been pressed");
      });
    return (
        <div>
            <div class="homebutton">
                <a href="/">FeedApp</a>
            </div>
        </div>
    );
  }

  export default HomeButton;

