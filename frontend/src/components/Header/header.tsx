import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
// import "./homeButton.css";
import HomeButton from "./buttons/homeButton";
import Login from "./Login/login";
import "./header.css"
import RegisterButton from "../Register/RegisterButton";

function Header() {
    createEffect(() => {
        console.log("Button has been pressed");
      });
    return (
        <div class="header-bar">
            <HomeButton />
            <Login />
            <RegisterButton />
            
        </div>
    );
  }

  export default Header;

