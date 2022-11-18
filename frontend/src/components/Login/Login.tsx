import { Component } from 'solid-js';
import { loginForm } from './LoginForm';
import RegisterButton from '../Register/RegisterButton';
import { useNavigate, NavLink } from '@solidjs/router';
import { createResource } from 'solid-js';
import "./login.css";

const Login: Component = () => {
    const { form, updateFormField, submit, clearField } = loginForm();
    const navigate = useNavigate();

    const handleSubmit = (e: Event) => {
        const data = submit(form);
        console.log(data);
        e.preventDefault();
        fetch(`${import.meta.env.VITE_BASE_URL}user/login`, {
                method: 'POST',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                    'Access-Control-Allow-Origin': '*'
                },
                body: JSON.stringify(data)
            }).catch(err => err)
            .then(response => {
                if (response.status === 200) {
                    console.log(response);
                    alert('Login successful');
                    return response.text();
                } else {
                    alert('Invalid credentials');
                }
            }
        )
            .then(data => {
                if(!data){return}
                localStorage.setItem("token", JSON.stringify(data));
                localStorage.setItem("loggedIn", JSON.stringify(true));
                navigate('/');

            })
        };
    

    return (
        <div class="container">
            <h1>Login</h1>
            <p> Please fill out the following information</p>
            <form class="form" onSubmit={handleSubmit}>
                <div class="form-group">
                    <label class="label" for="username">Username:</label>
                    <input 
                        type="text" 
                        class="form-control" 
                        id="username" 
                        placeholder="Enter username" 
                        value={form.userName}
                        onInput={updateFormField('userName')}
                        required 
                    />
                </div>
                <div class="form-group">
                    <label class="label password-label" for="password">Password:</label>
                    <input 
                        type="password" 
                        class="form-control" 
                        id="password" 
                        placeholder="Enter password"
                        value={form.password}
                        onInput={updateFormField('password')}
                        required 
                    />
                </div>

                <input class="form-submit submit-poll-btn" type="submit" value="Login" />
                <div class="form-submit">
                    Dont have an account? <NavLink href='/register' >Register here</NavLink>
                </div>
            </form>
        </div>
    )
}

export default Login;