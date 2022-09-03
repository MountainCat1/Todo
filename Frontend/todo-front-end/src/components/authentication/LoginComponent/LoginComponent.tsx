import '../../../styles/button.css'
import '../../../styles/form.css'
import './LoginComponent.css'
import React, {useState} from "react";
import {useNavigate} from "react-router-dom";
import { useCookies } from 'react-cookie'

interface LoginStatus {
    loading: boolean,
    error: boolean
}

interface LoginDto {
    username: string,
    password: string
}

export default function LoginComponent() {
    const [loginDto, setLoginDto] = useState<LoginDto>({username: "", password: ""});
    const [status, setStatus] = useState<LoginStatus>({
        loading: false,
        error: false
    });
    const navigate = useNavigate();
    const [, setCookie] = useCookies(['auth_token'])

    const handleChange = (e: React.FormEvent<HTMLInputElement>) => {
        e.preventDefault()
        setLoginDto({
            ...loginDto,
            [e.currentTarget.name]: e.currentTarget.value
        });
    }

    const handleSubmit = () => {
        console.log(`Submitting RegisterDto for user ${loginDto.username}`);

        postLoginDto(loginDto);
    }

    const handleLoginResponse = (loginResponse: string) => {
        setCookie('auth_token', loginResponse);
    }

    const postLoginDto = (dto: LoginDto) => {
        setStatus({...status, loading: true, error: false})

        const requestHeaders: HeadersInit = new Headers({
            'Content-Type': 'application/json'
        });
        const requestOptions: RequestInit = {
            method: 'POST',
            headers: requestHeaders,
            body: JSON.stringify(dto)
        };
        let url: string = `${process.env.REACT_APP_API_URL}/authentication/authenticate`;
        fetch(url, requestOptions)
            .then(response => {
                if (!response.ok) {
                    throw new Error(response.statusText)
                }
                return response.text();
            })
            .then((responseText) => {
                setStatus({...status, loading: false, error: false});

                handleLoginResponse(responseText);
                navigate('/');
            })
            .catch((ex) => {
                console.log(ex)
                setStatus({...status, loading: false, error: true});
            })
    }

    return (<div className='auth-panel'>
        <div>
            <h1>Welcome back!</h1>
            <div className='auth-panel-form'>
                <div className='input-group'>
                    <label>Username</label>
                    <input className='input-field' type="text" name='username' value={loginDto.username}
                           onChange={handleChange}/>
                </div>
                <div className='input-group'>
                    <label>Password</label>
                    <input className='input-field' type="password" name='password' value={loginDto.password}
                           onChange={handleChange}/>
                </div>
                <button
                    className='button'
                    onClick={() => {
                        handleSubmit()
                    }}>
                    Log In
                </button>
            </div>
            <div className='login-panel-result'>
                {
                    status.error
                        ? <div className='error-message'> Something went wrong! </div>
                        : status.loading
                            ? <div className='loading-ring'></div>
                            : null
                }
            </div>
        </div>
    </div>)
}