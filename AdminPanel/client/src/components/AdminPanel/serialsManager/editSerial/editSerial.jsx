import React from 'react';
import {useFormik} from "formik";
import * as yup from "yup";
import {updateFilm} from "../../../../actions/film";
import {updateSerial} from "../../../../actions/series";

const EditSerial = ({serial,setActive, allGenres}) => {
    const serialGenres = []

    serial.genrers.map((genre) => serialGenres.push(genre.GenreName))

    const formik = useFormik({
        initialValues:{
            title: serial.title,
            poster: serial.poster,
            description: serial.description,
            ageRating: serial.ageRation,
            genres: serialGenres,

        },validationSchema: yup.object({
            title: yup.string().required("*").max(50, 'Too long! maximum 50'),
            poster: yup.string().required("*"),
            description: yup.string().required("*").max(250, 'Too long! maximum 250'),
            ageRating: yup.string().required("*"),
            //genresAll: yup.string()

        }),
        enableReinitialize: true,
        onSubmit: async function (values) {
            await updateSerial(serial.Id,values.poster,values.title,0,values.ageRating,0,values.description,values.genres)
            formik.resetForm();
            setActive(false)


        }
    })

    return (
        <form onSubmit={formik.handleSubmit} onReset={formik.handleReset}>
            <div className="heading color-secondary-dark">EDIT SERIES</div>
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
                        allGenres.map((genre, index) => (
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

            <button type='submit' className="button dark color-green"> Submit
            </button>

            <button type="reset" className="button dark color-red" onClick={() => {
            }}> Reset
            </button>
        </form>

    )
};

export default EditSerial;