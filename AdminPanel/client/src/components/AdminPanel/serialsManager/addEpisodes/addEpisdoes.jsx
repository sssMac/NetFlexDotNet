import React, {useEffect, useState} from 'react';
import {ErrorMessage, Field, FieldArray, Form, Formik, useFormik} from "formik";
import * as yup from "yup";
import {addSeries} from "../../../../actions/series";
import {updateFilm} from "../../../../actions/film";
import {addEpisode, deleteEpisode, episodesBySerialId} from "../../../../actions/episode";
import axios from "axios";
import {useFormContext} from "react-hook-form";
import ReactPlayer from "react-player";

const validationSchema = yup.object().shape({
    episodes: yup.array().of(
        yup.object().shape({
            title: yup.string().required("*"),
            videoLink: yup.string().required("*"),
        })
    )
})

const AddEpisdoes = ({serial,setModalChild,setModalActive, setNumEp}) => {
    const [serEp, setSetEp] = useState([])


    useEffect(()=>{
        try{
            async function getEp(){
                const response = await axios.get(
                    `http://localhost:5000/episodes/episodesBySerialId?serialId=${serial.Id}`,
                );
                setSetEp(response.data)
            }
            getEp().then(r => r)
        }
       catch (e){

       }

    }, [serial])

    const episodes = []
    if(serEp !== null){
        serEp
            .sort((a, b) => a.Number > b.Number ? 1 : -1)
            .map((episode, index)=> {
            episodes.push({
                title: episode.Title,
                videoLink: episode.VideoLink,
                id: episode.Id
            })
        })
    }
    else{
        episodes.push({
            title: "",
            videoLink: "",
            id: ""
        })
    }




    console.log(serEp)


    return(
        <>
            <Formik
                initialValues={{
                    episodes: episodes
                }}
                validationSchema={validationSchema}
                enableReinitialize
                onSubmit={ async (values,{ resetForm }) => {
                    resetForm()
                    setModalActive(false)
                    for(var key in values.episodes){
                        await addEpisode(values.episodes[key].id,values.episodes[key].title, serial.Id,0,key,values.episodes[key].videoLink, serial.poster)
                    }

                }}
            >
                {(formik) => (
                    <Form>
                        <FieldArray
                            name="episodes"
                            render={(arrayHelpers) => {
                                return(
                                    <div>
                                        {formik.values.episodes
                                            .map((episode, index) => (
                                                <div className="horizontal" key={index}>
                                                    <div className="block color-dark" label="Title">
                                                        <Field
                                                            className="modal_input"
                                                            type="text"
                                                            placeholder="Title"
                                                            name={`episodes.${index}.title`}
                                                            id={`episodes.${index}.title`}
                                                        />
                                                        <div className="validation">
                                                            <div className="color-danger">
                                                                <ErrorMessage name={`episodes.${index}.title`} />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div className="block color-dark" label="Video Link">
                                                        <Field
                                                            className="modal_input"
                                                            type="url"
                                                            placeholder="Video Link"
                                                            name={`episodes.${index}.videoLink`}
                                                            id={`episodes.${index}.videoLink`}
                                                        />
                                                        <div className="validation">
                                                            <div className="color-danger">
                                                                <ErrorMessage name={`episodes.${index}.videoLink`} />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <button type="button"
                                                        className="button dark color-red delete"
                                                            onClick={() => {
                                                                deleteEpisode(episode.id).then(r => r)
                                                                arrayHelpers.remove(index)}}
                                                       >
                                                        DELETE
                                                    </button>
                                                    <button className="button dark color-light delete"
                                                            onClick={() => {
                                                                setModalActive(true)
                                                                setModalChild(<div className="text-primary color-dark">
                                                                    <div>
                                                                        {episode.title}
                                                                    </div>
                                                                    <ReactPlayer controls url={episode.videoLink} />
                                                                </div>)
                                                            }}>WATCH
                                                    </button>

                                                </div>
                                        ))}
                                        <button
                                            className="button dark color-green add"
                                            onClick={() => arrayHelpers.insert(formik.values.episodes.length + 1, { title: "", videoLink: ""})}>
                                            ADD
                                        </button>
                                    </div>

                                )
                            }}
                        />
                        <button type="submit" className="button dark color-green left">
                            Submit
                        </button>
                        <button type="reset" className="button dark color-red left">
                            Reset
                        </button>
                    </Form>
                )}

            </Formik>
        </>
    )
};

export default AddEpisdoes;