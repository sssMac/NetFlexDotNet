import React, {useEffect} from 'react';
import * as yup from "yup";
import {Formik, useFormik} from "formik";
import {addFilm} from "../../../../../actions/film";

const AddMovie = ({setModalActive,genres}) => {
    const formik = useFormik({
        initialValues:{
            title: "",
            poster: "",
            description:"",
            ageRating: "",
            videoLink: "",
            genres: [],

        },validationSchema: yup.object({
            title: yup.string().required("*").max(50, 'Too long! maximum 50'),
            poster: yup.string().required("*"),
            description: yup.string().required("*").max(250, 'Too long! maximum 250'),
            ageRating: yup.string().required("*"),
            videoLink: yup.string().required("*"),
            //genresAll: yup.string()

        }),
        onSubmit: async function (values) {
            setModalActive(false)
            formik.resetForm();
            await addFilm(values.poster,values.title,0,values.ageRating,0,values.description,values.videoLink,values.poster,values.genres)
            alert("successfully!")
            console.log(values.genresAll);

        }
    })

    return (
        <form onSubmit={formik.handleSubmit} onReset={formik.handleReset}>
            <div className="heading color-secondary-dark">ADD MOVIE</div>
            <div className="block color-dark" label="Title">
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
            <div className="block color-dark" label="Poster">
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
            <div className="block color-dark" label="VideoLink">
                <input
                    className="modal_input"
                    type="url"
                    placeholder="Video Link"
                    name="videoLink"
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    value={formik.values.videoLink}
                />
                <div className="validation">
                    <div className="color-danger">
                        {formik.touched.videoLink && formik.errors.videoLink ? (
                            formik.errors.videoLink
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
                        genres.map((genre, index) => (
                            <div key={index} className="modal_checkbox">
                                <input  name="genres"
                                        type="checkbox"
                                        id={genre.Id}
                                        value={genre.GenreName}
                                        checked={formik.values.genres.includes(genre.GenreName)}
                                        onChange={formik.handleChange}
                                        onBlur={formik.handleBlur}
                                />
                                <div className="label">{genre.GenreName}</div>
                            </div>

                        ))
                    }
                </div>
            </div>

            <button type="submit" className="button dark color-green"> Submit
            </button>

            <button type="reset" className="button dark color-red" onClick={() => {
                formik.resetForm();
            }}> Reset
            </button>
        </form>

    )
};

export default AddMovie;