import { NavLink } from '@solidjs/router';
import { Component } from 'solid-js';

const RegisterButton: Component = () => {
    return (
        <>
        <div class='register-btn'>
          <NavLink href='/register' >&#9760; Register </NavLink>
        </div>
      </>
    )
}

export default RegisterButton;