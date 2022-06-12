import React, {useState} from 'react';
import Input from "../../../../../utils/input/Input";
import {createRole} from "../../../../../actions/role";
import {publicReview} from "../../../../../actions/review";
import {useSelector} from "react-redux";
import {useFormik} from "formik";
import * as yup from "yup";
import {addFilm} from "../../../../../actions/film";


const PublicReview=({setActive, movies, series}) => {
    const user = useSelector(state => state.user)
    const formik = useFormik({
        initialValues:{
            selectContent: "",
            description: "",
            rating:"",


        },validationSchema: yup.object({
            selectContent: yup.string().required("*"),
            rating: yup.number().required("*").max(10,"Max value 10").min(0, "Min value 0"),
            description: yup.string().max(1250, 'Too long! maximum 2000').min(300, 'Too short! minimum 300'),

        }),
        onSubmit: async function (values) {
            setActive(false);
            formik.resetForm();
            await publicReview(user.currentUser.UserName, values.selectContent, values.description, values.rating)

        }
    })

    return (
        <form onSubmit={formik.handleSubmit} onReset={formik.handleReset}>
            <div className="heading color-secondary-dark">PUBLISH REVIEW</div>
            <div className="block" label="Select Content">
                <select
                    className="modal_input"
                    placeholder="Age Rating"
                    name="selectContent"
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    value={formik.values.selectContent}
                >
                    <option value="" label='Choose content...'> </option>
                    {
                        movies.map(movie => <option key={movie.Id} value={movie.Id}>{movie.title}</option>)
                    }

                    {
                        series.map(series => <option  key={series.Id} value={series.Id}>{series.title}</option>)
                    }

                </select>
                <div className="validation">
                    <div className="color-danger">
                        {formik.touched.selectContent && formik.errors.selectContent ? (
                            formik.errors.selectContent
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

            <div className="block color-dark" label="Rating">
                <input
                    className="modal_input"
                    type="number"
                    placeholder="Rating"
                    name="rating"
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    value={formik.values.rating}

                />
                <div className="validation">
                    <div className="color-danger">
                        {formik.touched.rating && formik.errors.rating ? (
                            formik.errors.rating
                        ) : null}
                    </div>
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

export default PublicReview;