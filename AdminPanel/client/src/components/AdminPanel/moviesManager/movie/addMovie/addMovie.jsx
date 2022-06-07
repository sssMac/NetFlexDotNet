import React, {useEffect} from 'react';
import * as yup from "yup";
import {useFormik} from "formik";
import {addFilm} from "../../../../../actions/film";

const AddMovie = ({setModalActive, genres}) => {
    const formik = useFormik({
        initialValues:{
            title: "",
            poster: "",
            description:"",
            ageRating: "",
            videoLink: "",
            genres: [],

        },validationSchema: yup.object({
            title: yup.string().required("*").max(50),
            poster: yup.string().required("*"),
            description: yup.string().required("*"),
            ageRating: yup.string().required("*"),
            videoLink: yup.string().required("*"),
            genres: []

        }),
        onSubmit:({title, description, ageRating,videoLink, poster,genresCheck}) => {
            console.log({title, description, genresCheck});
            //addFilm(poster,title,0,ageRating,0,description,videoLink,poster,genresCheck)
            setModalActive(false)
            formik.resetForm();
        }
    })

    return (
            <form onSubmit={formik.handleSubmit} onReset={formik.handleReset}>
                <div className="heading color-secondary-dark">ADD MOVIE</div>
                    <div className="block color-dark" data-label="Title">
                        <input
                            className="modal_input"
                            type="text"
                            placeholder="Title"
                            name="title"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.title}
                            autoComplete="off"

                        />
                        <div className="validation">
                            <div className="color-danger">
                                {formik.touched.title && formik.errors.title ? (
                                    formik.errors.title
                                ) : null}
                            </div>
                        </div>
                    </div>
                <div className="block color-dark" data-label="Poster">
                    <input
                        className="modal_input"
                        type="url"
                        placeholder="Poster"
                        name="poster"
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                        value={formik.values.poster}
                    />
                    <div className="validation">
                        <div className="color-danger">
                            {formik.touched.poster && formik.errors.poster ? (
                                formik.errors.poster
                            ) : null}
                        </div>
                    </div>
                </div>
                    <div className="block" label="Description">
                        <textarea
                            className="modal_input"
                            placeholder="Description"
                            name="description"
                            onChange={formik.handleChange}
                            onBlur={formik.handleBlur}
                            value={formik.values.description}
                        />
                        <div className="validation">
                            <div className="color-danger">
                                {formik.touched.description && formik.errors.description ? (
                                    formik.errors.description
                                ) : null}
                            </div>
                        </div>
                    </div>
                <div className="block" label="Age rating">
                    <select
                        className="modal_input"
                        placeholder="Age Rating"
                        name="ageRating"
                        onChange={formik.handleChange}
                        onBlur={formik.handleBlur}
                        value={formik.values.ageRating}
                    >
                        <option value="" label='Choose rating...'> </option>
                        <option value="0">0+</option>
                        <option value="6">6+</option>
                        <option value="12">12+</option>
                        <option value="18">18+</option>
                    </select>
                    <div className="validation">
                        <div className="color-danger">
                            {formik.touched.ageRating && formik.errors.ageRating ? (
                                formik.errors.ageRating
                            ) : null}
                        </div>
                    </div>
                </div>
                <div className="block" label="genres">
                    <div className="modal_checkbox_wrapper">
                            {
                                genres.map((genre) => (
                                    <div key={genre.Id} className="modal_checkbox">
                                        <input  name="genresCheck"
                                                id={genre.Id}
                                                type="checkbox"
                                                value={genre.value}
                                                checked={genre.value}
                                                onChange={formik.handleChange}
                                                onBlur={formik.handleBlur}
                                        />
                                        <div className="label">{genre.GenreName}</div>
                                    </div>
                                ))
                            }
                    </div>
                </div>

                <button type="submit" className="button dark color-green" onClick={() => {
                }}> Submit
                </button>

                <button type="reset" className="button dark color-red" onClick={() => {
                    formik.resetForm();
                }}> Reset
                </button>
            </form>
    )
};

export default AddMovie;