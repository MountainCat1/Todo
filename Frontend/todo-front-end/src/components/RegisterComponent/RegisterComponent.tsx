import './RegisterComponent.css'
import React from "react";

interface RegisterDto {
    username: string,
    password: string
}

interface RegisterRequestResponse {
    guid: String,
    username: String,
    userGuid: String
}

export class RegisterComponent extends React.Component<any, RegisterDto> {
    constructor(props: any) {
        super(props);

        this.state = {
            username: "",
            password: ""
        }

        this.handleChange = this.handleChange.bind(this);
    }

    handleChange(e: React.FormEvent<HTMLInputElement>) {
        e.preventDefault()
        this.setState({
            ...this.state,
            [e.currentTarget.name]: e.currentTarget.value
        });
    }

    handleSubmit() {
        console.log(`Submitting RegisterDto for user ${this.state.username}`);

        this.postRegisterDto(this.state);
    }

    postRegisterDto(dto: RegisterDto) {
        const requestHeaders: HeadersInit = new Headers({
            'Content-Type': 'application/json'
        });

        const requestOptions : RequestInit = {
            method: 'POST',
            headers: requestHeaders,
            body: JSON.stringify(dto)};

        let url : string = `${process.env.REACT_APP_API_URL}/authentication/register`; // `https://httpbin.org/post`;
        fetch(url, requestOptions)
            .then(response => {
                return response.json()
            })
            .then(responseJson => {
                let response = responseJson as RegisterRequestResponse;
                console.log(response.username);
            });
    }

    render() {
        return (<div className='Register-panel'>
            <div>
                <h2>Create your own account!</h2>
                <div>
                    <div className='input-group'>
                        <label>Username: </label>
                        <input className='input-field' type="text" name='username' value={this.state.username}
                               onChange={this.handleChange}/>
                    </div>
                    <div className='input-group'>
                        <label>Password: </label>
                        <input className='input-field' type="password" name='password' value={this.state.password}
                               onChange={this.handleChange}/>
                    </div>
                </div>
                <button
                    className='button'
                    onClick={() => {
                        this.handleSubmit()
                    }}>
                    Register
                </button>
            </div>

        </div>)
    }
}