import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./loginTest.css";

function LoginTest() {
    const testFunc = () => {
        console.log('added true to localstorage')
        if (!(localStorage.getItem("Test") === null)) {
            if (localStorage.getItem("Test") == JSON.stringify(true)) {
                localStorage.setItem("Test", JSON.stringify(false));
            }
            else {
                localStorage.setItem("Test", JSON.stringify(true));
            }
        }
        else {
            localStorage.setItem("Test", JSON.stringify(true));
        }
      }

    return (
        <div>
            <div class="loginbutton">
                <a href="/" onclick={testFunc}>Login Test</a>
            </div>
        </div>
    );
  }

  export default LoginTest;