import { Component } from 'solid-js';
import { registerForm } from './RegisterForm';

const Register: Component = () => {
    const { form, updateFormField, submit, clearField } = registerForm();

    const handleSubmit = (e: Event) => {
        const data = submit(form);
        e.preventDefault();
        console.log(data);   
    }

    return (
        <div class="container">
            <h1>Register</h1>
            <p> Please fill out the following information</p>
            <form onSubmit={handleSubmit}>
                <div class="form-group">
                    <label class="label" for="firstname">First name:</label>
                    <input 
                        type="text" 
                        class="form-control" 
                        id="firstname" 
                        placeholder="Enter first name" 
                        value={form.firstname}
                        onInput={updateFormField('firstname')}
                        required 
                    />
                </div>
                <div class="form-group">
                    <label class="label" for="lastname">Last name:</label>
                    <input 
                        type="text" 
                        class="form-control" 
                        id="lastname" 
                        placeholder="Enter last name" 
                        value={form.lastname}
                        onInput={updateFormField('lastname')}
                        required 
                    />
                </div>
                <div class="form-group">
                    <label class="label" for="username">Username:</label>
                    <input 
                        type="text" 
                        class="form-control" 
                        id="username" 
                        placeholder="Enter username" 
                        value={form.username}
                        onInput={updateFormField('username')}
                        required 
                    />
                </div>
                <div class="form-group">
                    <label class="label" for="email">Email:</label>
                    <input 
                        type="text" 
                        class="form-control" 
                        id="email" 
                        placeholder="Enter email" 
                        value={form.email}
                        onInput={updateFormField('email')}
                        required 
                    />
                </div>
                <div class="form-group">
                    <label class="label" for="password">Password:</label>
                    <input 
                        type="text" 
                        class="form-control" 
                        id="password" 
                        placeholder="Enter password"
                        value={form.password}
                        onInput={updateFormField('password')}
                        required 
                    />
                </div>
                <input class="form-submit reg-btn" type="submit" value="Register user" />
            </form>
        </div>
    )
}

export default Register;