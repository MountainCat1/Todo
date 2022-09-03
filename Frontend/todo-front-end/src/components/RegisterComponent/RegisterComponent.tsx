import './RegisterComponent.css'
import '../../styles/button.css'
import '../../styles/form.css'
import React from "react";
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import {useApiRegister, RegisterDto, RegistrationStatus} from "../../api/authentication";


export default function RegisterComponent() {
    const [registerDto, setRegisterDto] = useState<RegisterDto>({username: "", password: "" } );
    const [status, setStatus] = useState<RegistrationStatus>({
        loading: false,
        error: false
    });
    const navigate = useNavigate();
    const apiRegister = useApiRegister(setStatus, () => {
        navigate('/login')
    });

    const handleChange = (e: React.FormEvent<HTMLInputElement>) => {
        e.preventDefault()
        setRegisterDto({
            ...registerDto,
            [e.currentTarget.name]: e.currentTarget.value
        });
    }

    const handleSubmit = () => {
        console.log(`Submitting RegisterDto for user ${registerDto.username}`);

        apiRegister(registerDto);
    }

    return (<div className='auth-panel'>
        <div>
            <h1>Create your own account!</h1>
            <div className='auth-panel-form'>
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