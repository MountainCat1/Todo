import React, {useState} from "react";
import {CreateTeamDto, useApiCreateTeam} from "api/teamApi";
import {RequestStatus} from "api/abstractions";

interface ICreateTeamFormProps {
    closePopup : () => void
}

export default function CreateTeamForm(props : ICreateTeamFormProps){
    const [createDto, setCreateDto] = useState<CreateTeamDto>({
        name: '',
        description: ''
    })
    const [requestStatus, setRequestStatus] = useState<RequestStatus>({
        loading: false,
        error: false
    })
    const apiCreateTeam = useApiCreateTeam(setRequestStatus, () => {
        props.closePopup();
    });

    function handleChange(e : React.FormEvent<HTMLInputElement>) {
        e.preventDefault()
        setCreateDto({
            ...createDto,
            [e.currentTarget.name]: e.currentTarget.value
        });
    }

    async function handleSubmit() {
        await apiCreateTeam(createDto);
    }

    return (<div className='form'>
        <h1>Create team</h1>

        <div className='input-group'>
            <label>Team name</label>
            <input className='input-field' type="text" name='name' value={createDto.name}
                   onChange={handleChange}/>
        </div>
        <div className='input-group'>
            <label>Description</label>
            <input className='input-field' type="text" name='description' value={createDto.description}
                   onChange={handleChange}/>
        </div>
        <br/>

        <button className='button' onClick={handleSubmit}>
            Create Team
        </button>
        {
            <div className='form-result'>
                {
                    requestStatus.error
                        ? <div className='error-message'> Something went wrong! </div>
                        : requestStatus.loading
                            ? <div className='loading-ring'> </div>
                            : null
                }
            </div>

        }
    </div>)
}