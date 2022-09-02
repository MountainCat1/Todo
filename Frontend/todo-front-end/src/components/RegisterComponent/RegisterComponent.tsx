import './RegisterComponent.css'
import React from "react";
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

interface RegisterDto {
    username: string,
    password: string
}

interface RegisterRequestResponse {
    guid: String,
    username: String,
    userGuid: String
}

export default function() {
    const [registerDto, setRegisterDto] = useState<RegisterDto>({username: "", password: "" } );
    const [loading, setLoading] = useState<boolean>(false);

    const handleChange = (e: React.FormEvent<HTMLInputElement>) => {
        e.preventDefault()
        setRegisterDto({
            ...registerDto,
            [e.currentTarget.name]: e.currentTarget.value
        });
    }

    const handleSubmit = () => {
        console.log(`Submitting RegisterDto for user ${registerDto.username}`);

        postRegisterDto(registerDto);
    }

    const postRegisterDto = (dto: RegisterDto) => {
        setLoading(true);

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
            .catch(() => {
                setLoading(false);
            })
            .then(responseJson => {
                let response = responseJson as RegisterRequestResponse;
                console.log(response.username);
                setLoading(false);
            });
    }

    return (<div className='Register-panel'>
        <div>
            <h2>Create your own account!</h2>
            <div>
                <div className='input-group'>
                    <label>Username: </label>
                    <input className='input-field' type="text" name='username' value={registerDto.username}
                           onChange={handleChange}/>
                </div>
                <div className='input-group'>
                    <label>Password: </label>
                    <input className='input-field' type="password" name='password' value={registerDto.password}
                           onChange={handleChange}/>
                </div>
            </div>
            <button
                className='button'
                onClick={() => {
                    handleSubmit()
                }}>
                Register
            </button>
            {
                loading ? <div className='loading-ring'> </div> : null
            }
        </div>

    </div>)
}