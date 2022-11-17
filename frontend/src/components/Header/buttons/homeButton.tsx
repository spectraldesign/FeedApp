import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "../../button.css";

function HomeButton() {
    
    return (
        <div>
            <div class="button homebutton">
                <a href="/">FeedApp</a>
            </div>
        </div>
    );
  }

  export default HomeButton;

