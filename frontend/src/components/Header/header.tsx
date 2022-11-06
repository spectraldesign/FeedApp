import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
// import "./homeButton.css";
import HomeButton from "./buttons/homeButton";
import Login from "./Login/login";
import "./header.css"
import RegisterButton from "../Register/RegisterButton";
import ProfileButton from "./Profile/profileButton";

function Header() {
    if (!(localStorage.getItem("Test") === null)) {
        if (localStorage.getItem("Test") == JSON.stringify(true)) {
            return (
                <div class="header-bar">
                    <HomeButton />
                    <ProfileButton />
                </div>
            )
        }
        else {
            return (
                <div class="header-bar">
                <HomeButton />
                <Login />
                </div>
            );
        }
    }
    return (
        <div class="header-bar">
            <HomeButton />
            <Login />
        </div>
    );
  }

  export default Header;

