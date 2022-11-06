import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./homeButton.css";

function HomeButton() {
    
    return (
        <div>
            <div class="homebutton">
                <a href="/">FeedApp</a>
            </div>
        </div>
    );
  }

  export default HomeButton;

