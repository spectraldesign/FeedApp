import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./profile.css";

function Profile() {
    return (
        <div>
            <div class="profile-page">
                <div class="circle">
                    <div class="picture">
                        <i class='fas fa-user-alt' style='font-size:80px;color:white;'></i>
                    </div>
                </div>
                {/* <input class="picture" value="&#9786;"></input> */}
                <div class="centerer">
                    <div class="firstname">
                        Full Name
                    </div>
                </div>
                <div class="centerer">
                    <div class="username">
                        Username
                    </div>
                </div>
                <div class="centerer">
                    <div class="email">
                        Email
                    </div>
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

