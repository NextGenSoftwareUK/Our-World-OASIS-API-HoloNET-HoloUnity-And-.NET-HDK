import React from 'react';
import { connect } from 'react-redux';
import { login } from '../actions/auth';
import '../CSS/Login.css';
const axios = require('axios');

class Login extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            email: '',
            password: ''
        }
        console.log(this.props)
    }

    dispatch = this.props.dispatch
    
    handleLogin = (e) => {
        e.preventDefault();

        let data = {
            email: this.state.email,
            password: this.state.password
        }

        this.dispatch(login(data))
        
    }

    handleEmailChange = (event) => {
        this.setState({email: event.target.value});
    }

    handlePasswordChange = (event) => {
        this.setState({password: event.target.value});
    }

    render() {
        return (
            <form className="login-form" onSubmit={this.handleLogin}>
                <div className="login-title">
                    <h1 className="login-header">Log In</h1>

                    <p className="login-title-text">
                        Don't have an account? <span onClick={this.props.change} className="link">Sign Up!</span>
                    </p>
                </div>

                <div className="login-inputs">
                    <label htmlFor="login-email">EMAIL</label>
                    <input value={this.state.email} onChange={this.handleEmailChange} type="email" placeholder="name@example.com" />
            
                    <label htmlFor="login-password">PASSWORD</label>
                    <input type="password" value={this.state.password} onChange={this.handlePasswordChange} />
                
                    <label className="link">Forgot Password?</label>
                    <div>
                        <input type="checkbox" name="remember-login" />
                        <label htmlFor="remember-login">Remember Me</label>
                    </div>

                    <button type="submit" className="login-submit">Submit</button>
                </div>
            </form>
        )
    }
}

export default connect()(Login)