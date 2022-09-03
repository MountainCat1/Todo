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

interface RegistrationStatus {
    loading: boolean,
    error: boolean
}

export default function RegisterComponent() {
    const [registerDto, setRegisterDto] = useState<RegisterDto>({username: "", password: "" } );
    const [status, setStatus] = useState<RegistrationStatus>({
        loading: false,
        error: false
    });
    const navigate = useNavigate();

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
        setStatus({...status, error: false, loading: true});

        const requestHeaders: HeadersInit = new Headers({
            'Content-Type': 'application/json'
        });

        const requestOptions : RequestInit = {
            method: 'POST',
            headers: requestHeaders,
            body: JSON.stringify(dto)};

        let url : string = `${process.env.REACT_APP_API_URL}/authentication/register`;
        fetch(url, requestOptions)
            .then(response => {
                return response.json()
            })
            .catch(() => {
                setStatus({...status, loading: false, error: true});
            })
            .then(responseJson => {
                let response = responseJson as RegisterRequestResponse;
                console.log(response.username);
                setStatus({...status, loading: false, error: false});
                navigate('/login');
            });
    }

    return (<div className='Register-panel'>
        <div>
            <h2>Create your own account!</h2>
            <div className='register-panel-form'>
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
                <button
                    className='button'
                    onClick={() => {
                        handleSubmit()
                    }}>
                    Register
                </button>
            </div>
            <div className='register-panel-result'>
                {
                    status.error
                        ? <div className='error-message'> Something went wrong! </div>
                        : status.loading
                            ? <div className='loading-ring'> </div>
                            : null
                }
            </div>


        </div>

    </div>)
}