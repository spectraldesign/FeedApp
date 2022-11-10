import { Navigate, useNavigate } from "@solidjs/router";
import { createSignal, createEffect, createMemo } from "solid-js";
// import { css } from 'emotion';
import "./Logout.css";

function Logout() {
    const logoutFunc = () => {
        localStorage.clear();
        alert('Logout successful');
      }
    
    return (
        <div>
            <div class="logout">
                <a href="/login" class="logout-btn" onClick={logoutFunc}>Logout</a>
            </div>
        </div>
    );
  }

  export default Logout;

