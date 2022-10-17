--
-- PostgreSQL database dump
--

-- Dumped from database version 14.5
-- Dumped by pg_dump version 14.5

-- Started on 2022-10-13 15:35:43 AEDT

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'SQL_ASCII';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 212 (class 1259 OID 16499)
-- Name: art_styles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.art_styles (
    style_id integer NOT NULL,
    name character varying(50) NOT NULL,
    url character varying(200) NOT NULL,
    created_date timestamp without time zone NOT NULL,
    modified_date timestamp without time zone NOT NULL
);


ALTER TABLE public.art_styles OWNER TO postgres;

--
-- TOC entry 211 (class 1259 OID 16498)
-- Name: art_styles_style_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.art_styles ALTER COLUMN style_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.art_styles_style_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 210 (class 1259 OID 16400)
-- Name: artists; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.artists (
    artist_id integer NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    year_born integer NOT NULL,
    created_date timestamp without time zone NOT NULL,
    modified_date timestamp without time zone NOT NULL
);


ALTER TABLE public.artists OWNER TO postgres;

--
-- TOC entry 209 (class 1259 OID 16399)
-- Name: artists_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.artists ALTER COLUMN artist_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.artists_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 214 (class 1259 OID 16505)
-- Name: artwork; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.artwork (
    art_id integer NOT NULL,
    title character varying(50) NOT NULL,
    artist_id integer NOT NULL,
    url character varying(200) NOT NULL,
    created_date timestamp without time zone NOT NULL,
    modified_date timestamp without time zone NOT NULL
);


ALTER TABLE public.artwork OWNER TO postgres;

--
-- TOC entry 213 (class 1259 OID 16504)
-- Name: artwork_art_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.artwork ALTER COLUMN art_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.artwork_art_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 216 (class 1259 OID 16516)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id integer NOT NULL,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    role character varying(20),
    created_date timestamp without time zone NOT NULL,
    modified_date timestamp without time zone NOT NULL,
    password_hash character varying(100),
    email character varying(100)
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16515)
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.users ALTER COLUMN id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.users_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 3448 (class 2606 OID 16503)
-- Name: art_styles art_styles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.art_styles
    ADD CONSTRAINT art_styles_pkey PRIMARY KEY (style_id);


--
-- TOC entry 3450 (class 2606 OID 16509)
-- Name: artwork artwork_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.artwork
    ADD CONSTRAINT artwork_pkey PRIMARY KEY (art_id);


--
-- TOC entry 3446 (class 2606 OID 16404)
-- Name: artists pk_artists; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.artists
    ADD CONSTRAINT pk_artists PRIMARY KEY (artist_id);


--
-- TOC entry 3452 (class 2606 OID 16520)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- TOC entry 3453 (class 2606 OID 16510)
-- Name: artwork fk_artist; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.artwork
    ADD CONSTRAINT fk_artist FOREIGN KEY (artist_id) REFERENCES public.artists(artist_id);


-- Completed on 2022-10-13 15:35:43 AEDT

--
-- PostgreSQL database dump complete
--

