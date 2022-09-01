import './RegisterComponent.css'
import React from "react";
import {Simulate} from "react-dom/test-utils";

interface RegisterDto {
    username: string,
    password: string
}

export class RegisterComponent extends React.Component<any, RegisterDto>{
    constructor(props: any) {
        super(props);

        this.state = {
           password: "",
           username: ""
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

    handleSubmit(){
        console.log(`Submitting RegisterDto for user ${this.state.username}`);

        this.postRegisterDto(this.state);
    }

    postRegisterDto(dto: RegisterDto){
        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(dto)
        };
        // TODO NEED TO CHANGE URL!
        fetch('https://jsonplaceholder.typicode.com/posts', requestOptions)
            .then(response => {
                console.log(response);
            });
    }

    render() {
        return(<div className='Register-panel'>
            <form>

            </form>

            <input type="text" name='username' value={this.state.username}      onChange={this.handleChange}/>
            <input type="password" name='password' value={this.state.password}  onChange={this.handleChange} />
            <button
                onClick={(event) => {
                    this.handleSubmit()
                }}
            >
                Register
            </button>
        </div>)
    }
}