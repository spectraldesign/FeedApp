import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./SearchPoll.css";

function SearchPoll() {
    return (
        <div class="trial">
            <div class="poll-search">
                <form class="poll-search-form" action="">
                    <input class="poll-search-input" type="text" id="fname" name="firstname" placeholder="&#128269; Enter Poll ID"></input>
                    <input class="submit-poll-btn" type="submit" value="Enter"></input>
                </form>
            </div>
        </div>
    );
  }

  export default SearchPoll;

