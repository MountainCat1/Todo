import React, {useState} from "react";
import {CreateTeamDto} from "../../../../api/teamApi";


export default function CreateTeamForm(){

    const [createDto, setCreateDto] = useState<CreateTeamDto>({
        name: '',
        description: ''
    })

    function handleChange(e : React.FormEvent<HTMLInputElement>) {
        e.preventDefault()
        setCreateDto({
            ...createDto,
            [e.currentTarget.name]: e.currentTarget.value
        });
    }

    function handleSubmit() {
        console.log(createDto)
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
    </div>)
}