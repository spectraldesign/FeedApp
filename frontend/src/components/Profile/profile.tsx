import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./profile.css";

function Profile() {
    return (
        <div>
            <div class="profile-page">
                <div class="picture">
                    &#9786;
                </div>
                {/* <input class="picture" value="&#9786;"></input> */}
                <div class="firstname">
                    First Name
                </div>
                <div class="username">
                    Username
                </div>
                <div class="email">
                    Email
                </div>
                <div class="bottom">
                    <div class="votes">94,000 votes</div>
                    <div class="seperator">|</div>
                    <div class="polls">3,120 polls</div>
                </div>
            </div>
        </div>
    );
  }

  export default Profile;

