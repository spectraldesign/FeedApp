import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
// import "./homeButton.css";
import HomeButton from "./buttons/homeButton";
import Login from "./Login/login";
import "./headerloggedin.css"
import RegisterButton from "../Register/RegisterButton";
import ProfileButton from "./Profile/profileButton";

function HeaderLoggedIn() {
    
    return (
        <div class="header-bar">
            <HomeButton />
            <ProfileButton />
            
        </div>
    );
  }

  export default HeaderLoggedIn;

