import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./loginTest.css";

function LoginTest() {
    createEffect(() => {
        console.log("Button has been pressed");
    });
    const [loggedIn, setLoggedIn] = createSignal(false);
    const toggle = () => setLoggedIn(!loggedIn());

    return (
        <div>
            <div class="loginbutton">
                <a href="/">Login Test</a>
            </div>
        </div>
    );
  }

  export default LoginTest;